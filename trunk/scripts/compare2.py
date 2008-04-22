import os
import md5

def matches(hash, compareTo):
  return compareTo[0] == hash

files = os.listdir(".")

sums = open("sums").readlines()
for i in range(len(sums)):
  sums[i] = sums[i].strip().split()

for f in files:
  fp = open(f)
  hash = md5.new(fp.read()).hexdigest()
  fp.close()

  numMatches = 0
  for g in sums:
    if matches(hash, g):
      numMatches += 1
      print '\tstrcpy(path[%s], "%s.duplicate");' % (f.split(".")[-1], g[1])
      #print str(numMatches) + ". " + f + " matches " + g[1]

