FROM node:12

WORKDIR /tests

# Copy package*.json separately to allow Docker layer caching when package.json is unchanged
COPY ["/tests/package*.json", "./"]
RUN npm install

COPY ["/tests", "./"]
CMD ["/bin/bash", "-c", "./runtests.sh"]
