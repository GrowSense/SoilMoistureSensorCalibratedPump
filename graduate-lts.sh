#!/bin/bash

BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')

if [ "$BRANCH" = "dev" ];  then
  git checkout master || exit 1
fi

echo "Graduating master branch to lts branch"

echo ""
echo "Fetching other branches from origin..."
git fetch origin && \

echo ""
echo "Pulling the master branch from origin (to update it locally)..."
git pull origin master && \

echo ""
echo "Merging the lts branch into the master branch..."
git merge lts && \

echo ""
echo "Checking out the lts branch..."
git checkout lts && \

echo ""
echo "Pulling the lts branch from origin to update it..."
git pull origin lts && \

echo ""
echo "Merging the master branch into the lts branch..."
git merge master && \

echo ""
echo "Pushing the updated lts branch to origin..."
git push origin lts && \

echo ""
echo "Checking out the $BRANCH again..."
git checkout $BRANCH && \

echo "The 'master' branch has been graduated to the 'lts' branch"
