# how to use GIT

## first time configuration
1. git config --global user.email <your email>
2. git config --global user.name <your name>

## first time init

1. git init .
2. git remote add <link name> <repisitory link>
    * git remote add origin https://sandykho47@bitbucket.org/sandykho47/sdl.git
3. git pull -u <link nama> <branch name>
    * git pull -u origin master

## commit & push

1. git add .
2. git commit -m "<nama commit>"
    * git commit -m "avl animation insert"
3. git push <link name> <branch name>
    * git push origin master

## extra syntax

* git log
* git status

## link ref
* https://medium.com/learn-git-today/the-ultimate-git-guide-to-creating-your-first-repo-b50762a6ab00