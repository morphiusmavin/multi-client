#make -f ts4600_$1.mak clean
rm *.o &> out.txt
rm sched150 &> out.txt
make -f ts4600_$1.mak &> out1.txt
if grep -q error out1.txt
 then
  find2 error out1.txt
  exit 1
fi

if grep -q undefined out1.txt
 then
  find2 undefined out1.txt
  exit 1
fi
if [ -e sched$1 ]
 then
   rm *.o
   mv sched$1 /home/dan/dev/sched/sched
   ls -ltr /home/dan/dev/sched
   exit 0
fi
echo "something went wrong with sched"
cat out1.txt
exit 1
