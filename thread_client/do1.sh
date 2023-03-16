clear
#make -f ts4600_$1.mak clean
rm *.o &> out.txt
rm sched150 &> out.txt
make -f ts4600_$1.mak &> out.txt
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
if [ -e sched$1 ]
 then
   rm *.o
   mv sched$1 /home/dan/dev/sched/sched
   ls -ltr /home/dan/dev/sched
   exit 1
fi
echo "something went wrong..."
cat out.txt
