# syntax=docker/dockerfile:1

FROM node:16-alpine3.12 AS frontBuild
WORKDIR /src
ARG FRONTEND_APP_SRC_URL=https://github.com/PSW-2021-ORG7/Pharmacy/archive/refs/heads/master.tar.gz
SHELL ["/bin/ash", "-o", "pipefail", "-c"]
RUN apk --update --no-cache add \
    curl \
    tar \
    gettext && \
    curl -L "${FRONTEND_APP_SRC_URL}" | tar -xz && \
    mv "$(find . -maxdepth 1 -type d | tail -n 1)" app && \
    cd app/frontend && \
    npm install
COPY ./files/environment.ts.template ./app
COPY ./files/build-app.sh .
ARG PHARMACY_API_URL
RUN chmod +x ./build-app.sh && \
    ./build-app.sh ${PHARMACY_API_URL}

FROM nginx:1.19.8-alpine AS gatewayWithFront
COPY --from=frontBuild /app /usr/share/nginx/html/app
COPY ./files/nginx/nginx.conf /etc/nginx/nginx.conf
COPY ./files/nginx/api_gateway.conf /etc/nginx/api_gateway.conf