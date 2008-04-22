from PIL import Image
import sys

SP2={"ARLI.SPR":["ARLI2.SP2"],
"BEHI.SPR":["BEHI2.SP2"],
"BIBUROS.SPR":["BIBU2.SP2"],
"BOM.SPR":["BOM2.SP2"],
"DEMON.SPR":["DEMON2.SP2"],
"DORA2.SPR":["DORA22.SP2"],
"HYOU.SPR":["HYOU2.SP2"],
"TETSU.SPR":["IRON2.SP2","IRON3.SP2","IRON4.SP2","IRON5.SP2"],
"MINOTA.SPR":["MINOTA2.SP2"],
"MOL.SPR":["MOL2.SP2"],
"TORI.SPR":["TORI2.SP2"],
"URI.SPR":["URI2.SP2"]}

#define DEC_SIZE   25600
DEC_SIZE = 25600
wnibble_stat = False
nibble_str = None

#BYTE *nibble_str;
#BYTE dest[DEC_SIZE];

rnibble_pos = 0
data=""

#BYTE GetNibble()
#{
def GetNibble():
  global rnibble_pos
  if (rnibble_pos & 0x01) == 0: val = ord(nibble_str[rnibble_pos/2]) >> 4
  else: val = ord(nibble_str[rnibble_pos/2])& 0x0F
  rnibble_pos += 1
  return val

def SprDec(source, dest):
  global wnibble_stat, rnibble_pos, nibble_str
  dest_index = 0
  wnibble_stat = False
  rnibble_pos = 0
  nibble_str = source
  i = 0
  while rnibble_pos/2 < len(source):
    nibble = GetNibble()
    if nibble != 0:
      if wnibble_stat:
        dest[dest_index] = dest[dest_index] | nibble
        dest_index += 1
        i += 1
      else: dest[dest_index] = nibble << 4
      wnibble_stat = not wnibble_stat
    else:
      nibble = GetNibble()
      if nibble == 8: length = GetNibble()|(GetNibble() << 4)|(GetNibble() << 8)
      elif nibble == 0: length = GetNibble()
      elif nibble == 7: length = GetNibble()|(GetNibble() << 4)
      else: length = nibble
      for j in range(length):
        if i >= len(dest): dest.append(0)
        if wnibble_stat:
          dest_index += 1
          dest[i] = dest[i] & 0xF0
          i += 1
        else: dest[i] = 0
        wnibble_stat = not wnibble_stat
    i += 1
    
def upper(b):
  return (b&0xF0) >> 4

def lower(b):
  return b&0x0F

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
    im.putpixel((i%256,i/256), palette[pixels[i]])
  return True
  
h=488

fn = sys.argv[1]
print fn
data = open(fn).read()
dest=[0] * 25600

compress_start = 0x9200
if fn == "KASANEK.SPR" or fn == "KASANEM.SPR":
  compress_start = 0x8200
if len(data) >= compress_start and ord(data[compress_start]) != 0:
  SprDec(data[compress_start:], dest)

for i in range(len(dest)):
  dest[i]= chr(dest[i])

others=""
if SP2.has_key(fn):
  for sp2 in SP2[fn]:
    others += open(sp2).read()
    h += 256
    
bytes=data[16*32:compress_start] + ''.join(dest) + others

pixels = buildPixels(bytes)

size=(256,h)

for i in range(16):
  im = Image.new("RGBA", size, (0,0,0,0))
  if drawPixelsWithPaletteOnImage(pixels, buildPalette(data[i*32:(i+1)*32]), im):
    im.save("%s.palette%02d.png" % (fn, i), "PNG")