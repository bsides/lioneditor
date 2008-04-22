from PIL import Image
import sys

SP2={"ARLI.SPR":["ARLI2.SP2"],"BEHI.SPR":["BEHI2.SP2"],"BIBUROS.SPR":["BIBU2.SP2"],"BOM.SPR":["BOM2.SP2"],
"DEMON.SPR":["DEMON2.SP2"],"DORA2.SPR":["DORA22.SP2"],"HYOU.SPR":["HYOU2.SP2"],
"TETSU.SPR":["IRON2.SP2","IRON3.SP2","IRON4.SP2","IRON5.SP2"],
"MINOTA.SPR":["MINOTA2.SP2"],"MOL.SPR":["MOL2.SP2"],"TORI.SPR":["TORI2.SP2"],"URI.SPR":["URI2.SP2"]}

def upper(b): return (b&0xF0) >> 4
def lower(b): return b&0x0F
def SprDec(source):
  temp = []
  # Build a list of ints from the nibbles
  for i in range(len(source)):
    temp.append(upper(ord(source[i])))
    temp.append(lower(ord(source[i])))
  i = 0
  result = []
  while i < len(temp):
    if temp[i] != 0:
      result.append(temp[i])
    elif (i + 1) < len(temp):
      s = temp[i+1]
      l = s
      if s == 0 and (i + 2) < len(temp): 
        l = temp[i+2]
        i += 1
      elif s == 7 and (i + 3) < len(temp): 
        l = temp[i+2] + (temp[i+3] << 4)
        i += 2
      elif s == 8 and (i + 4) < len(temp): 
        l = temp[i+2] + (temp[i+3] << 4) + (temp[i+4] << 8)
        i += 3
      i += 1
      # pad a bunch of 0's
      for j in range(l): result.append(0)
    i += 1
  i = 0
  # flip each pair of bytes
  while (i+1) < len(result):
    j = result[i]
    result[i] = result[i+1]
    result[i+1] = j
    i += 2
  return result

def getRGB(bytes):
  b = (ord(bytes[1]) & 0x7C) << 1
  g = ((ord(bytes[1]) & 0x03) << 6) | ((ord(bytes[0]) & 0xE0) >> 2)
  r = (ord(bytes[0]) & 0x1F) << 3
  a = 255
  return (r,g,b,a)

def buildPalette(bytes):
  colors = []
  for i in range(16):
    colors.insert(len(colors), getRGB(bytes[i*2:i*2+2]))
  if colors[0] == (0,0,0,255):
    colors[0] = (0,0,0,0)
  return colors

def buildPixels(bytes):
  result=[]
  for b in bytes:
    result.append(lower(ord(b)))
    result.append(upper(ord(b)))
  return result

emptyPalette = [(0,0,0,0),(0,0,0,255),(0,0,0,255),(0,0,0,255),
(0,0,0,255),(0,0,0,255),(0,0,0,255),(0,0,0,255),
(0,0,0,255),(0,0,0,255),(0,0,0,255),(0,0,0,255),
(0,0,0,255),(0,0,0,255),(0,0,0,255),(0,0,0,255)]

def drawPixelsWithPaletteOnImage(pixels, palette, im):
  if palette == emptyPalette:
    return False
  for i in range(len(pixels)):
    try:
      im.putpixel((i%256,i/256), palette[pixels[i]])
    except:
      e=0
  return True

fn = sys.argv[1]
print fn
data = open(fn).read()

compress_start = 0x9200
if fn == "KASANEK.SPR" or fn == "KASANEM.SPR":
  compress_start = 0x8200

h=0
decompressed = []
if len(data) >= compress_start and ord(data[compress_start]) != 0:
  decompressed = SprDec(data[compress_start:])
  h = 488
else:
  h = 288
  
others=""
if SP2.has_key(fn):
  for sp2 in SP2[fn]:
    others += open(sp2).read()
    h += 256
    
bytes=data[16*32:compress_start]

pixels = buildPixels(bytes)
for i in range(len(decompressed)):
  pixels.append(decompressed[i])

otherPixels = buildPixels(others)
for i in range(len(otherPixels)):
  pixels.append(otherPixels[i])
    
size=(256,h)

for i in range(16):
  im = Image.new("RGBA", size, (0,0,0,0))
  if drawPixelsWithPaletteOnImage(pixels, buildPalette(data[i*32:(i+1)*32]), im):
    im.save("%s.palette%02d.png" % (fn, i), "PNG")