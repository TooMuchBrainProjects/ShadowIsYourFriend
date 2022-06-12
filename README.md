# ShadowIsYourFriend
This is the official repository of the all over the world known and most famous game forever Shadow Is Your Friend.

## How to use Git with Unity
https://thoughtbot.com/blog/how-to-git-with-unity

## rel branch
If you want to create and push a new release, checkout to main branch, pull, 
copy every file ***except .gitignore*** to the rel branch (that means you also have to checkout to rel branch), 
go in Unity Editor, build the new version and 
push to rel branch.

The .gitignore file from rel branch will automatically ignore all files which are not necessary for the release!

If you aren't familiar with git, these code fragments will help you:

    git checkout main
  
    git pull
    
Now you need to copy all files **except .gitignore** from this directory in explorer to rel branch.
Then you need to build the new version in Unity Editor.

    git status
    
    git add .
    git commit -m "release v1.0.0" <-- [your version]
    git push
