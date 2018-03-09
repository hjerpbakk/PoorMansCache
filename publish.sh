#! /bin/bash
set -e

VERSION=$(cat VERSION.txt)

./build.sh
docker tag dips/poormans-cache:latest vt-optimus-solr02:5000/dips/poormans-cache:$VERSION
docker push vt-optimus-solr02:5000/dips/poormans-cache:$VERSION
