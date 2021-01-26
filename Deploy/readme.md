# About

 - inventories/main/hosts is the inventory containing the server IP
 - you need ssh key
 - ping with: `ansible -i ./inventories/main -m ping --extra-vars 'ansible_ssh_private_key_file=ansible.key' all` 
