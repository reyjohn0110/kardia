@echo off
pushd "%1"
echo Pushing contents of the folder %1
echo ---------------------------------
echo [Adding Files]
git add --all --verbose
echo ---------------------------------
echo [Committing Files]
git commit -m "Update %date% / %time%"
echo ---------------------------------
echo [Pushing Changes]
git push -u -v
echo [Done]
pause