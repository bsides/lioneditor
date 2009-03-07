us=open("us.md5").readlines()
jp=open("jp.md5").readlines()
psp=open("fftpack.md5").readlines()

us_dict={}
jp_dict={}
psp_dict={}

for i in range(len(us)):
	[key,value]=us[i].split()
	us_dict[key]=value
	us[i]=[key,value]

for i in range(len(jp)):
	[key,value]=jp[i].split()
	jp_dict[key]=value
	jp[i]=[key,value]

for i in range(len(psp)):
	[key,value]=psp[i].split()
	psp_dict[key]=value.strip()
	psp[i]=[key,value.strip()]

common=[]

for i in range(len(psp)):
	found=False
	usEntry=""
	jpEntry=""
	if (us_dict.has_key(psp[i][0])):
		usEntry=us_dict[psp[i][0]]
		found=True
	
	if (jp_dict.has_key(psp[i][0])):
		jpEntry=jp_dict[psp[i][0]]
		found=True
	
	common.append([psp_dict[psp[i][0]], usEntry, jpEntry])

print common