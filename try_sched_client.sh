echo "running try_sched.sh" >> status.txt
date >> status.txt

if [ -e /home/dan/sched ]
then
 mv /home/dan/sched .
 chmod +x sched
 echo "new sched found in ~" >> status.txt
else
 echo "using current sched" >> status.txt
fi

if [ -e /home/dan/sock_mgt ]
then
 mv /home/dan/sock_mgt .
 chmod +x sock_mgt
 echo "new sock_mgt found in ~" >> status.txt
else
 echo "using current sock_mgt" >> status.txt
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

if [ $OUT -eq 6 ]
 then
  while [ $OUT -eq 6 ]
  do 
  echo "start new sched w/o reboot" >> status.txt
	mv /home/dan/sched .
	chmod +x sched 
  ./sched odata.dat  
  $OUT=$?
  done 
fi

