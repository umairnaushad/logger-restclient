import fileinput
import time

list_minute=[]
list_count=[]

def parseMinutes(fileName):
    count = 1
    minute_last=""
    for line in fileinput.input(fileName):
        minute_current=":".join(line.split(":", 2)[:2])
        if(minute_current==minute_last):
            count+=1
        else:
            list_minute.append(minute_last)
            list_count.append(count)
            count=1
            minute_last=minute_current

    list_minute.append(minute_last)
    list_count.append(count)

def parseMessages(fileName):
    return

def printParsedMinutes():
    list_length=len(list_minute)
    for i in range(1,list_length):
        print("%s,%s"%(list_minute[i], list_count[i]))

def printParsedMessages():
    return





start = time.time()
parseMinutes('log_file1.txt')
printParsedMinutes()
end = time.time()
print("Execution time in seconds: ",(end - start))