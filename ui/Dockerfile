FROM node:24-alpine3.21 AS build
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build -- --configuration production

FROM nginx:alpine AS production

RUN rm /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist/cdb-yield-simulator/browser/ /usr/share/nginx/html/
COPY nginx.conf /etc/nginx/conf.d/

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
