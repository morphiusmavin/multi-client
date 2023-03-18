#make clean
rm *.o
rm sched
rm sock_mgt
make &> out.txt
if grep -q error out.txt
 then
  find2 error out.txt
  exit 1
fi

if grep -q undefined out.txt
 then
  find2 undefined out.txt
  exit 1
fi
rm *.o
tar cf server.tar sched sock_mgt
scp server.tar 192.168.88.146:/home/dan
exit 0
