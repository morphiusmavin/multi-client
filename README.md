multi-client
server has 2 processes: sock_mgt & sched
sock_mgt handles all tcp and sched handles io
each process passes queue msg's to each other

client (sched) gets tcp msg's from server and
handles it's own io

