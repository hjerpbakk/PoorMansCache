#! /bin/bash
set -e

VERSION=$(cat VERSION.txt)

./build.sh
sudo docker tag dips/poormans-cache:latest vt-optimus-solr02:5000/dips/poormans-cache:$VERSION
sudo docker push vt-optimus-solr02:5000/dips/poormans-cache:$VERSION
