from PIL import Image

def getRGB(bytes):
  b = (bytes[1] & 0x7C) << 1
  g = ((bytes[1] & 0x03) << 6) | ((bytes[0] & 0xE0) >> 2)
  r = (bytes[0] & 0x1F) << 3
  a = 255
  return (r,g,b,a)

def buildPalette(bytes, offset):
  colors = []
  for i in range(16):
    colors.insert(len(colors), getRGB(bytes[offset+i*2:offset+i*2+2]))
  if colors[0] == (0,0,0,255):
    colors[0] = (0,0,0,0)
  return colors
  
def getPalettes(stream, offset, number):
  result=[]
  for i in range(number):
    result += [buildPalette(stream, offset+16*2*i)]
  return result
  
def getBytes(filename):
  f = open(filename,"rb")
  b = f.read()
  f.close()
  result = [0]*len(b)
  for i in range(len(b)):
    result[i] = ord(b[i])
  return result

def upper(b): return (b&0xF0) >> 4
def lower(b): return b&0x0F

def swapNibbles(bytes):
  result = [0]*len(bytes)*2
  for i in range(len(bytes)):
    result[i*2]=lower(bytes[i])
    result[i*2+1]=upper(bytes[i])
  return result

def getPaletteForCoordinate(palettes, x, y):
  if y<260:
    #print "using %d" % (x/24+y/40)
    return palettes[x/24 + y/40*10]
  elif x < 20 and y < 295:
    return palettes[85]
  else: return palettes[0]
  
bytes = getBytes("unit.bin")

palettes=getPalettes(bytes, 0xF000, 128)

width=256
height=0xF000/256*2

bytes=swapNibbles(bytes[:0xF000])
im = Image.new("RGBA", (width,height), (0,0,0,0))

for j in range(len(palettes)):
    im = Image.new("RGBA", (width,height), (0,0,0,0))
    for i in range(len(bytes)):
      try:
        #palette=getPaletteForCoordinate(palettes, i%width,i/width)
        palette=palettes[j]
        im.putpixel((i%width,i/width), palette[bytes[i]])
      except IndexError:
        print "",
    im.save("unit%03d.bin.png"%(j), "PNG")