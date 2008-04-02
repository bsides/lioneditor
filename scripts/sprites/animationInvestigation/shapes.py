from PIL import Image

def charsToInts(chars):
  result = [0] * len(chars)
  for i in range(len(chars)):
    result[i] = ord(chars[i])
  return result

def bytesToInt(bytes):
  result = 0
  result += bytes[0]
  result += bytes[1] * 256
  result += bytes[2] * 256 * 256
  result += bytes[3] * 256 * 256 * 256
  return result
  
def expandNibbles(bytes):
  result = [0] * len(bytes) * 2
  for i in range(len(bytes)):
    result[i*2] = upper(bytes[i])
    result[i*2+1] = lower(bytes[i])
  return result

def bytesToChars(bytes):
  chars = []
  for i in range(len(bytes)):
    chars.append(chr(bytes[i]))
  return ''.join(chars)
  
def processShapes():
	bytes = charsToInts(open("KANZEN.SHP").read())
	
	positions = [0]
	pos=4
	b=[]
	while (pos < len(bytes) and bytes[8+pos:8+pos+4] != [ 0x00, 0x00, 0x00, 0x00 ]):
		positions.append(bytesToInt(bytes[8+pos:8+pos+4]))
		pos += 4
	for i in xrange(len(positions)):
		if i==(len(positions)-1):
			b.append(bytes[0x408+positions[i]:])
		else:
			b.append(bytes[0x408+positions[i-1]:0x408+positions[i]])
		print b[-1]
	return b
	result = []
	for i in xrange(len(b)):
		r=[]
		for j in range(0,len(b[i]),2):
			r.append((b[i][j],b[i][j+1]))
		result.append(r)		

	return result

def paintGrid(im):
  tileWidth=35
  tileHeight=40
  # 48 x 54
  for row in xrange(0,488,2):
    for col in range(0,256,tileWidth):
	  im.putpixel((col,row),(0,0,0,255))
  for col in xrange(0,256,2):
    for row in range(0,488,tileHeight):
	  im.putpixel((col,row),(0,0,0,255))
	  
def copyRectangleToPoint(sourceImage, x, y, width, height, destImage, destX, destY):
	print x,y
	for row in xrange(height):
		for col in xrange(width):
			#print col,row
			p = sourceImage.getpixel((x+col,y+row))
			if p != (0,0,0,0):
				destImage.putpixel((destX+col,destY+row), p)
				
def innerGetFrames(bytes, imageIn):
	start = 0x40A
	numberOfFrames = 0x100
	for frameNumber in xrange(0,numberOfFrames):
		im = Image.new("RGBA", (512,512), (0,0,0,0))
		frameStart = start + bytesToInt(bytes[0x08+frameNumber*0x04:0x08+frameNumber*0x04+4])
		print "frameStart: %08X" % (frameStart)
		numberOfTile = bytesToInt(bytes[frameStart:frameStart+2] + [0,0])
		print "numberOfTile: %08X" % (numberOfTile)
		rects = []
		for tileNumber in xrange(0,numberOfTile+1):
			x=(bytes[frameStart+0x02+tileNumber*0x04]+127)&0xFF
			y=(bytes[frameStart+0x02+tileNumber*0x04+1]+127)&0XFF
			flags = bytesToInt(bytes[frameStart+0x02+tileNumber*4+2:frameStart+0x02+tileNumber*4+2+2] + [0,0])
			f = (flags>>10) & 0x0F
			if f==0x06:
				width=24
				height=24
			elif f==0x08:
				width=32
				height=16
			elif f==0x09:
				width=32
				height=24
			elif f==0x0A:
				width=32
				height=32
			elif f==0x0B:
				width=32
				height=40
			elif f==0x0D:
				width=48
				height=48
			elif f==0x0E:
				width=56
				height=48
			else:
				print "Unknown sprite value %04X" % ((flags>>10)&0x0F)
				continue
			tileX = (flags&0x001F) * 8
			tileY = ((flags>>5)&0x001F) * 8
			rects.append((imageIn, tileX, tileY, width, height, im, x, y))
			#copyRectangleToPoint(imageIn, tileX, tileY, width, height, im,x, y)
		rects.reverse()
		for r in rects:
			copyRectangleToPoint(r[0], r[1], r[2], r[3], r[4], r[5], r[6], r[7])
		im.save("out%d.png" % (frameNumber), "PNG")


im=Image.open("KANZEN.SPR.palette00.png")
innerGetFrames(charsToInts(open("KANZEN.SHP").read()), im)
s=processShapes()
#for i in range(len(s)):
#for j in range(len(s[i])):
#    im.putpixel(s[i][j], (255,255,0,255))
paintGrid(im)
im.save("output.png", "PNG")