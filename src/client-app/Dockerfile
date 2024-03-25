# => Build container
FROM node:alpine as builder
WORKDIR  /app
COPY  package.json .
RUN yarn install
COPY . .
ENV REACT_APP_NAME=myName
COPY .env .
RUN yarn build

# => Run container
FROM nginx:1.15.2-alpine

# Nginx config
RUN rm -rf /etc/nginx/conf.d
COPY conf /etc/nginx

# Static build
COPY --from=builder /app/build /usr/share/nginx/html/

# Default port exposure
EXPOSE 80

# Copy .env file and shell script to container
WORKDIR /usr/share/nginx/html
# COPY ./env.sh .


# # Start Nginx server
# CMD ["/bin/sh", "-c", "nginx -g \"daemon off;\""]
