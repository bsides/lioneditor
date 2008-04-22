from PIL import Image

colors=[(0,0,0,0), (70, 63, 51, 255), (107, 104, 85, 255), (120,112,96,255)]
#colors = [(255,255,255,255), (0,0,0,255), (0,0,0,255), (0,0,0,255)]

def getBytes(filename):
  f = open(filename,"rb")
  b = f.read()
  f.close()
  result = [0]*len(b)
  for i in range(len(b)):
    result[i] = ord(b[i])
  return result

def getColors(bytes, index):
  result = [0] * 4
  val=bytes[index]
  result[0] = (val&0xC0) >> 6
  result[1] = (val&0x30) >> 4
  result[2] = (val&0x0C) >> 2
  result[3] = (val&0x03)
  return result
  
def getGlyph(bytes, index):
  result=[]
  for i in range(35):
    colors = getColors(bytes, index+i)
    for j in colors: result.append(j)
  return result

def paintGlyph(image, glyph, x, y):
  for i in range(len(glyph)):
    image.putpixel((x+i%10,y+i/10), colors[glyph[i]])
    
def buildPNG(filename, bytes):
  im = Image.new("RGBA", (48*(10+1),46*(14+1)), (0,0,0,0))
  for i in range(2200):
    paintGlyph(im, getGlyph(bytes, i*35), (i%48)*11, (i/48)*15)
  im.save(filename, "PNG")

buildPNG("psp.png", getBytes("pspfont.bin"))
buildPNG("psx.png", getBytes("psxfont.bin"))
