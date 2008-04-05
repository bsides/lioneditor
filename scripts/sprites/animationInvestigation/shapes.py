from PIL import Image
from PIL import ImageDraw
import sys
import os.path

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
	imArray = im.load()
	for i in range(len(pixels)):
		try: imArray[i%256,i/256] = palette[pixels[i]]
		except: e=0
	return True
  
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

def copyRectangleToPoint(sourceImage, x, y, width, height, destImage, destX, destY, mirror):
	#print x,y,width,height
	sourceArray = sourceImage.load()
	destArray = destImage.load()
	if destX < 0 or destY < 0 or destX+width>destImage.size[0] or destY+height>destImage.size[1]:
		print destX, destY
	if not mirror:
		for row in xrange(height):
			for col in xrange(width):
				#print col,row
				p = sourceArray[x+col,y+row]
				if p[3] != 0: destArray[destX+col, destY+row] = p
	else:
		# TODO : handle mirroring
		for row in xrange(height):
			for col in xrange(width):
				#print col,row
				p = sourceArray[x+col,y+row]
				if p[3] != 0: destArray[destX+(width-col-1), destY+row] = p
				
def splitImage(im):
	result = Image.new("RGBA", im.size, (0,0,0,0))
	#def copyRectangleToPoint(sourceImage, x, y, width, height, destImage, destX, destY, mirror):
	copyRectangleToPoint(im, 0, 0, 256, 256, result, 0, 0, False)
	copyRectangleToPoint(im, 0, 288, 256, 200, result, 0, 256, False)
	copyRectangleToPoint(im, 0, 256, 256, 32, result, 0, 456, False)

	if im.size[1] > 488:
		copyRectangleToPoint(im, 0, 488, 256, im.size[1]-488, result, 0, 488, False)
	result.save("split.png", "PNG")
	return result
	
def byteToBin(b):
	r=""
	for i in range(7,-1,-1):
		r += str((b&(1<<i)) >> i)
	return r
	
def logInfo(index, bytes):
	try:
		goods.index(index)
		hex=[]
		for b in bytes:
			hex.append("%02X" % (b))
		hex=" ".join(hex)
		print "%03d:" % (index),
		print hex,
		print " ",
		for b in bytes:
			print byteToBin(b),
		print "",
		fbin = byteToBin(bytes[3]) + byteToBin(bytes[2])
		print fbin[0:2] + " " + fbin[2:6] + " " + fbin[6:11] + " " + fbin[11:]
	except:
		return

goods=[52]
counter = -1

def getFrames(file, imageIn, wep=False):
	if wep:
		start = 0x802
	else:
		start = 0x402
		
	jump = bytesToInt(file[0:4])
	#print jump
	secondHalf = bytesToInt(file[4:6]+[0,0])
	thirdHalf = bytesToInt(file[6:8]+[0,0])
	if jump > 8:
		n=innerGetFrames(file[8:jump], imageIn, secondHalf, thirdHalf, start)
		innerGetFrames(file[jump:], imageIn, secondHalf, thirdHalf, start, n)
	else:
		if wep:
			innerGetFrames(file[0x44:], imageIn, secondHalf, thirdHalf, start)
		else:
			innerGetFrames(file[8:], imageIn, secondHalf, thirdHalf, start)

fToSize = { 0:(8,8),
1: (16,8), 2:(16,16), 3:(16,24), 
4:(24,8), 
#4:(24,24),
5:(24,16), 6:(24,24), 7:(32,8), 8:(32,16), 9:(32,24), 0xA:(32,32), 0xB:(32,40), 0xC:(48,16), 0xD:(40,32), 0xE:(48,48), 0xF:(56,56) }

def innerGetFrames(bytes, imageIn, secondHalf, thirdHalf, start, startingFrameNumber=0):
	numberOfFrames = 0x100
	global goods
	global counter
	for frameNumber in xrange(startingFrameNumber,numberOfFrames+startingFrameNumber):
		im = Image.new("RGBA", (210,160), (0,0,0,0))
		frameStart = start+bytesToInt(bytes[(frameNumber-startingFrameNumber)*0x04:(frameNumber-startingFrameNumber)*0x04+4])
		if frameStart==start and frameNumber != startingFrameNumber:
			break
		numberOfTile = bytesToInt(bytes[frameStart:frameStart+2] + [0,0])
		rects = []
		for tileNumber in xrange(0,numberOfTile+1):
			counter += 1
			xbyte=bytes[frameStart+0x02+tileNumber*0x04]
			ybyte=bytes[frameStart+0x02+tileNumber*0x04+1];
			x=abs(xbyte-129)
			y=abs(ybyte-129)
			flags = bytesToInt(bytes[frameStart+0x02+tileNumber*4+2:frameStart+0x02+tileNumber*4+2+2] + [0,0])
			mirror = ((flags & 0x4000) == 0x4000)
			f = (flags>>10) & 0xF
			
			
			if fToSize.has_key(f):
				(width, height) = fToSize[f]
			else:
				print "%d Unknown sprite value %04X" % (counter, (flags>>10)&0x0F)
				#continue
				width=4
				height=4
			tileX = (flags&0x001F) * 8
			tileY = ((flags>>5)&0x001F) * 8
			if (frameNumber-startingFrameNumber) >= secondHalf:
				tileY+=256
			if (frameNumber-startingFrameNumber) >= secondHalf+thirdHalf:
				tileY-=256
			# if (frameNumber-startingFrameNumber) >= secondHalf+thirdHalf+16:
				# tileY-= 232
			# if (frameNumber-startingFrameNumber) >= secondHalf+thirdHalf+16+83:
				# tileY-=256
			#if (frameNumber==25):
			goods.append(counter)
			logInfo(counter, bytes[frameStart+0x02+tileNumber*0x04:frameStart+0x02+tileNumber*0x04+4])
			
			
			big = Image.open("dongs.png")
			draw = ImageDraw.Draw(big)
			draw.rectangle([(tileX, tileY), (tileX+width,tileY+height)], outline=(255,255,0))
			big.save("big%04d.png"%counter, "png")
			
			
			#print "%02X %02X %d %d %d %d %d %d" % (xbyte, ybyte, xbyte, ybyte, width, height, (flags&0x8000)>>15, (flags&0x4000)>>14)
			
			rects.append((imageIn, tileX, tileY, width, height, im, x, y, mirror))
		rects.reverse()
		for r in rects:
			copyRectangleToPoint(r[0], r[1], r[2], r[3], r[4], r[5], r[6], r[7], r[8])
		im.save("out%d.png" % (frameNumber), "PNG")
	return frameNumber

SP2={"ARLI.SPR":["ARLI2.SP2"],"BEHI.SPR":["BEHI2.SP2"],"BIBUROS.SPR":["BIBU2.SP2"],"BOM.SPR":["BOM2.SP2"],
"DEMON.SPR":["DEMON2.SP2"],"DORA2.SPR":["DORA22.SP2"],"HYOU.SPR":["HYOU2.SP2"],
"TETSU.SPR":["IRON2.SP2","IRON3.SP2","IRON4.SP2","IRON5.SP2"],
"MINOTA.SPR":["MINOTA2.SP2"],"MOL.SPR":["MOL2.SP2"],"TORI.SPR":["TORI2.SP2"],"URI.SPR":["URI2.SP2"]}
	
def main(argv=None):
	if argv is None:
		argv = sys.argv
	fn = argv[1]
	print fn
	print argv[2]
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
	if SP2.has_key(os.path.split(fn)[-1]):
		for sp2 in SP2[os.path.split(fn)[-1]]:
			others += open(os.path.join(os.path.split(fn)[0],sp2)).read()
			h += 256
	 
	bytes=data[16*32:compress_start]

	pixels = buildPixels(bytes)
	for i in range(len(decompressed)):
		pixels.append(decompressed[i])
		
	otherPixels = buildPixels(others)
	for i in range(len(otherPixels)):
		pixels.append(otherPixels[i])
		
	size=(256,max(488,h))

	im = Image.new("RGBA", size, (0,0,0,0))
	drawPixelsWithPaletteOnImage(pixels, buildPalette(data[0:32]), im)

	im2=splitImage(im)
	shp=charsToInts(open(argv[2]).read())

	im3=im2.copy()
	replaceTransparentWithBlack(im3)
	im3.save("dongs.png", "PNG")

	wep=False
	try:
		argv[1].index("WEP")
		print "weapons" 
		wep=True
	except:
		a=0
	getFrames(shp, im2, wep)

if __name__ == "__main__":
    sys.exit(main())