# About

 - inventories/main/hosts is the inventory containing the server IP
 - you need ssh key
    - add ansible key to the ssh-agent:
        - `eval "$(ssh-agent -s)"`
        - `ssh-add ansible.key`
 - ping with: `ansible -i ./inventories/main -m ping --extra-vars 'ansible_ssh_private_key_file=ansible.key' all`
    - expected output:
```
69.90.132.97 | SUCCESS => {
    "ansible_facts": {
        "discovered_interpreter_python": "/usr/bin/python3"
    },
    "changed": false,
    "ping": "pong"
}
```


## Playbooks

 - setup infrastructure: `ansible-playbook -i ./inventories/main --extra-var 'ansible_ssh_private_key_file=ansible.key' infrastructure.yml`
 - deploy webapp: `ansible-playbook -i ./inventories/main --extra-var 'ansible_ssh_private_key_file=ansible.key' webapp.yml`
