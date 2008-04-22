from charmap import charmap
from charmap import getNextChar
from charmap import stringToBytes
from charmap import bytesToString

def processPointer(bytes):
  length = ((bytes[0]&0x03) << 3) + ((bytes[1]&0xE0)>>5) + 4
  j = ((bytes[1]&0x1F)<<8) + bytes[2];
  return length, j-(j/256)*2
  
bytes=stringToBytes(open("WLDHELP.LZW").read()[0x80:])
lengths={}
jumps={}

for i in xrange(36): lengths[i]=0
for i in xrange(8192): jumps[i]=0
i = 0
while i < len(bytes):
  if (bytes[i]&0xFC)==0xF0:
    length,jump = processPointer(bytes[i:i+3])
    lengths[length] +=1
    jumps[jump] +=1
    i += 2
  i += 1
  
#print lengths
#print jumps

for i in xrange(3791,8191): 
  if jumps[i]!=0: print i