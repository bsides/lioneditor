import sys

def upper(b): return (b&0xF0) >> 4
def lower(b): return b&0x0F

def charsToInts(chars):
  result = [0] * len(chars)
  for i in range(len(chars)):
    result[i] = ord(chars[i])
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
  
def compressNibbles(bytes):
  result = [0] * (len(bytes)/2)
  for i in range(0,len(bytes),2):
    if (i+1) < len(bytes): result[i/2] = ((bytes[i]&0x0F) << 4) + (bytes[i+1] & 0x0F)
    else: result.append((bytes[i]&0x0F) << 4)
  return result

def numberOfZeroes(bytes):
  for i in range(len(bytes)):
    if bytes[i] != 0:
      return i
  return len(bytes)

infile = open("output.bin").read()
infile = charsToInts(infile)
infile = expandNibbles(infile)

result = []


pos = 0
while pos < len(infile):
  zeroes = numberOfZeroes(infile[pos:])
  if zeroes == 0:
    result.append(infile[pos])
    pos += 1
  elif zeroes < 16:
#    if len(result)%2 == 0:
#      result.append(0)
#      result.append(zeroes)
#    else:
      result.append(0)
      result.append(0)
      result.append(zeroes)
  elif zeroes < 256:
    result.append(0)
    result.append(7)
    result.append(lower(zeroes))
    result.append(upper(zeroes))
  elif zeroes < 4096:
    result.append(0)
    result.append(8)
    result.append(lower(zeroes))
    result.append(upper(zeroes))
    result.append((zeroes&0xF00) >> 8)
  pos += zeroes

open("result.bin","w").write(bytesToChars(compressNibbles(result)))