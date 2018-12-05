# Convert console to form

1. Add new form
2. Add/change to this
```cs
using System.Windows.Forms;

namespace Btree
{
    class Program
    {
        [STAThread]
        static void Main()
        {   
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

```
3. Right Click on Project > Properties > Set output type "Windows application"

# How to use GIT

## First Time Configuration
1. git config --global user.email <your email>
2. git config --global user.name <your name>
    
## First time - init

1. git init .
2. git remote add <link name> <repisitory link>
    * git remote add origin https://sandykho47@bitbucket.org/sandykho47/sdl.git
3. git pull <link nama> <branch name>
    * git pull origin master

## Commit & push

1. git add .
2. git commit -m "<nama commit>"
    * git commit -m "avl animation insert"
3. git push <link name> <branch name>
    * git push origin master

## Commit & push alternative

1. git add .
2. git commit -m "<nama commit>"
    * git commit -m "avl animation insert"

## Extra Syntax

* git log - lihat semua commit yang ada di local
* git status - lihat status (file yang diubah, file yang belum di add, file yang sudah di add, dll)

## List error & Fix

* error: failed to push some refs to 'https://sandykho47@bitbucket.org/sandykho47/sdl.git'
    * cara 1
        * git pull --rebase=preserve origin master
        * git push origin master
    * cara 2 (semua dilakukan di branch yang dipakai)
        * git fetch origin
        * git merge remotes/origin/master
        * git push origin master
* remove remote url
    * git remote rm <remote name>

## Extra

* Only push latest commit to remote repisitory
    * git push origin HEAD:master

## Link Ref
* https://medium.com/learn-git-today/the-ultimate-git-guide-to-creating-your-first-repo-b50762a6ab00
* https://stackoverflow.com/questions/24114676/git-error-failed-to-push-some-refs-to
* https://help.github.com/articles/removing-a-remote/
* https://www.atlassian.com/git/tutorials/syncing/git-fetch