import fileinput
import time
import sys

"""
Following is the decleration of data structures to be used during processing
"""
# List to store minute value/name (including date) against each unique minute
list_minute=[]
# list to store total number of matched minutes against each unique minute
list_count=[]
# List of list to store message count against each line
list_process_count=[]
process_names = ["Safari[4589]", "syslogd[139]", "Microsoft Remote Desktop[4395]", "systemstats[150]", "timed[180]"]
process_count = [0,0,0,0,0]

# To clear data from list
def flushCount():
    for i in range(len(process_count)):
        process_count[i]=0

# To count total number of messages in a minute and store it in lists
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

# To count each type of message in a minute and store it in lists
def parseMessages(fileName):
    count = 1
    minute_last=""
    for line in fileinput.input(fileName):
        minute_current=":".join(line.split(":", 2)[:2])

        if(minute_current==minute_last):
            count+=1
        else:
            list_minute.append(minute_last)
            list_count.append(count)
            list_process_count.append(process_count.copy())
            flushCount()
            count=1
            minute_last=minute_current

        for i in range(len(process_names)):
            if process_names[i] in line:
                process_count[i]+=1
                break

    list_minute.append(minute_last)
    list_count.append(count)
    list_process_count.append(process_count.copy())

# To print total number of messages in a minute
def printParsedMinutes():
    print("###################################")
    print("minute,number_of_messages")
    list_length=len(list_minute)
    for i in range(1,list_length):
        print("%s,%10s"%(list_minute[i], list_count[i]))

# To print each type of message in a minute
def printParsedMessages():
    print("###################################")
    print("minute", end = '')
    for i in range(len(process_names)):
        print(",%s"%(process_names[i]), end = '')
    print()
    minute_length=len(list_minute)
    for i in range(1,minute_length):
        print("%s"%(list_minute[i]), end = '')
        for j in range(len(process_count)):
            print(",%s"%(list_process_count[i][j]), end = '')
        print()



"""
Verifying that file to be parsed is passed as argument.
Call functions to parse data and print it on console,
Also print the total execution time.
"""
if len(sys.argv) < 2:
    print("Invalid arguments, please pass valid text file name")
    sys.exit()
#argv[0] is name of the running program
fileName=sys.argv[1]
print("fileName:"+fileName)

start = time.time()
parseMessages(fileName)
printParsedMinutes()
printParsedMessages()
end = time.time()
print("Execution time in seconds: ",(end - start))