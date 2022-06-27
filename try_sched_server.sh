echo "running try_sched.sh (v1.14)" >> status.txt
date >> status.txt

if [ -e /home/dan/sched ]
then
 mv /home/dan/sched .
 chmod +x sched
 mv /home/dan/sock_mgt .
 chmod +x sock_mgt
 echo "new sched found in /root" >> status.txt
else
 echo "using current sched" >> status.txt
fi

./sock_mgt &
./sched odata.dat

OUT=$?
echo "OUT:" >> status.txt
echo $OUT >> status.txt
date >> status.txt

if [ $OUT -eq 1 ]
 then
  echo "exit to shell" >> status.txt
fi

if [ $OUT -eq 2 ]
 then
  echo "rebooting from script" >> status.txt
 /sbin/reboot
fi

if [ $OUT -eq 3 ]
 then
  echo "shutdown from script" >> status.txt
/sbin/shutdown -h now
fi

