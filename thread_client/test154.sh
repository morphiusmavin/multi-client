if [[ -z "${CROSS_COMPILE}" ]]; then
  echo "run setARMpath.sh"
  exit 1
fi
if [ -e ~/thread_client.zip ]
then
unzip -o ~/thread_client.zip
else
echo "thread_client.zip not found"
exit 1
fi
if [ -e ~/Client154.zip ]
then
cd Client154
unzip -o ~/Client154.zip
cd ..
else
echo "Client154.zip not found"
exit 1
fi
make -f make154.mak clean
make -f make154.mak &> out.txt

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
scp sched154 192.168.88.154:/home/dan/sched
