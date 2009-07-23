import Image
import ImageDraw
import sys

def upper(b): return (b&0xF0) >> 4
def lower(b): return b&0x0F

def getRGB(bytes):
  b = (bytes[1] & 0x7C) << 1
  g = ((bytes[1] & 0x03) << 6) | ((bytes[0] & 0xE0) >> 2)
  r = (bytes[0] & 0x1F) << 3
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
    result.append(lower(b))
    result.append(upper(b))
  return result

def drawPixelsWithPaletteOnImage(pixels, palette, im):
  imArray = im.load()
  for i in xrange(len(pixels)):
    try: imArray[i%256,i/256] = palette[pixels[i]]
    except: e=0
  return True

def drawPixelsWithPaletteOnImageRect(pixels, palette, im, x, y, width, height):
  imArray = im.load()
  for myx in xrange(width):
    for myy in xrange(height):
      imArray[myx,myy] = palette[pixels[(myx+x)+(myy+y)*256]]

def replaceTransparentWithBlack(im):
  imArray = im.load()
  for row in xrange(im.size[1]):
    for col in xrange(im.size[0]):
      p = imArray[col,row]
      if p[3] == 0: imArray[col,row] = (0,0,0,255)

def charsToInts(chars):
  result = [0] * len(chars)
  for i in range(len(chars)):
    result[i] = ord(chars[i])
  return result

fn = "FRAME.BIN"  
if (len(sys.argv) > 5):
  fn = sys.argv[1]
  
f=open(fn,"rb")
bytes=f.read()
f.close()
bytes = charsToInts(bytes)
pixels = buildPixels(bytes[:0x9000])

palettes = [0]*22
for i in xrange(len(palettes)):
  palettes[i] = buildPalette(bytes[0x9000+32*i:0x9000+32*(i+1)])

top_size = (256,32)
top_slice = pixels[:256*32]
bottom_size = (256,256)
bottom_slice = pixels[256*32:]
entire_size = (256,288)

def dump_images():
  for i in xrange(len(palettes)):
    im = Image.new("RGBA", top_size, (0,0,0,0))
    drawPixelsWithPaletteOnImage(top_slice, palettes[i], im)
    im.save("FRAME.BIN.top.%02d.png" % (i), "PNG")
  for i in xrange(len(palettes)):
    im = Image.new("RGBA", bottom_size, (0,0,0,0))
    drawPixelsWithPaletteOnImage(bottom_slice, palettes[i], im)
    im.save("FRAME.BIN.bottom.%02d.png" % (i), "PNG")

layout="""00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 42 00 6A 00 1C 00 08 00 40 00 F0 00 15 00 08 00 40 00 52 00 22 00 08 00 62 00 52 00 26 00 08 00 70 00 5A 00 1A 00 08 00 60 00 62 00 28 00 08 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 96 00 70 00 0A 00 08 00 A8 00 55 00 15 00 08 00 00 00 62 00 1C 00 08 00 00 00 6A 00 2E 00 08 00 8A 00 55 00 1E 00 08 00 00 00 00 00 00 00 00 00 7E 00 20 00 13 00 09 00 00 00 00 00 00 00 00 00 00 00 52 00 1A 00 08 00 5E 00 6A 00 16 00 08 00 96 00 68 00 1F 00 08 00 00 00 5A 00 12 00 08 00 12 00 5A 00 16 00 08 00 28 00 5A 00 13 00 08 00 2E 00 6A 00 13 00 08 00 00 00 00 00 00 00 00 00 3C 00 5A 00 13 00 08 00 4F 00 5A 00 20 00 08 00 1C 00 62 00 18 00 08 00 A4 00 5D 00 14 00 08 00 38 00 62 00 28 00 08 00 24 00 D2 00 20 00 08 00 9C 00 CC 00 1C 00 08 00 74 00 6A 00 22 00 0E 00"""
layout=''.join(layout.split())
battlebinlayout="""B6 70 12 08 8A 5D 1A 08 42 6A 1C 0A 40 F0 15 08 
40 52 22 08 62 52 26 08 70 5A 1A 08 60 62 28 08 
96 70 0A 08 A8 55 15 08 00 62 1A 08 00 6A 2E 08 
8A 55 1E 08 00 52 1A 08 5E 6A 16 0A 96 68 1F 08 
00 5A 12 08 12 5A 16 08 28 5A 13 08 2E 6A 13 08 
3C 5A 13 08 4F 5A 20 08 1C 62 18 08 A4 5D 14 0A 
38 62 28 08 24 D2 20 08 9C CC 1D 08 74 6A 22 0E 
1C 52 19 08 B8 CC 1B 08"""
battlebinlayout=''.join(battlebinlayout.split())

def hexToByte(bytestring):
  upper = int(bytestring[0],16)
  lower = int(bytestring[1],16)
  return (upper << 4) | lower

new_layout = [0] * (len(layout)/2)
for i in xrange(len(layout)/2):
  new_layout[i] = hexToByte(layout[i*2:i*2+2])
layout=new_layout

new_bbin_layout = [0]*(len(battlebinlayout)/2)
for i in xrange(len(battlebinlayout)/2):
  new_bbin_layout[i] = hexToByte(battlebinlayout[i*2:i*2+2])
battlebinlayout=new_bbin_layout

def getXYWidthHeightBattleBin(bytes):
  return (bytes[0], bytes[1], bytes[2], bytes[3])
  
def getXYWidthHeight(bytes):
  x = bytes[0] + bytes[1]*16
  y = bytes[2] + bytes[3]*16
  width = bytes[4]+bytes[5]*16
  height = bytes[6]+bytes[7]*16
  return (x,y,width,height)

rectangles = [0] * (len(layout)/8)
for i in xrange(len(layout)/8):
  rectangles[i] = getXYWidthHeight(layout[8*i:8*i+8])

bbin_rectangles = [0] * (len(battlebinlayout)/4)
for i in xrange(len(battlebinlayout)/4):
  bbin_rectangles[i] = getXYWidthHeightBattleBin(battlebinlayout[4*i:4*i+4])

nice_palette = palettes[10]

#for i in xrange(len(rectangles)):
def dump_rectangles():
  for i in xrange(len(bbin_rectangles)):
    #r = rectangles[i]
    r = bbin_rectangles[i]
    im = Image.new("RGBA", bottom_size, (0,0,0,0))
    drawPixelsWithPaletteOnImage(bottom_slice, nice_palette, im)
    if (r[2]==0 and r[3] == 0): 
      continue
    print ("(%d,%d) %dx%d"%( r[0],r[1],r[2],r[3]))
    draw = ImageDraw.Draw(im)
    draw.rectangle([(r[0],r[1]),(r[2]+r[0],r[3]+r[1])], outline=(255,0,0))
    del draw
    im.save("FRAME.BIN.rectangles.%02d.png"%(i),"PNG")
    
if __name__ == "__main__":
  myx = int(sys.argv[2],16)
  myy = int(sys.argv[3],16)
  mywidth=int(sys.argv[4],16)
  myheight=int(sys.argv[5],16)
  im = Image.new("RGBA", (mywidth,myheight),(0,0,0,0))
  drawPixelsWithPaletteOnImageRect(bottom_slice, nice_palette, im, myx, myy, mywidth, myheight)
  im.save("out.png","PNG")