# Use Node.js as the base image
FROM node:20 AS development

# Set the working directory
WORKDIR /angular-app

# Copy package files and install dependencies
COPY gamestore-ui-app/package*.json ./
RUN npm install

# Copy the Angular project files
COPY . .

# Expose the development port
EXPOSE 4200

# Start the Angular app in development mode
CMD ["npm", "run", "start", "--", "--host", "0.0.0.0"]
