// Main entry point for Express.js.
const express = require('express');
const bodyParser = require('body-parser');
const mongoose = require('mongoose');
const loanRoutes = require('./Routes/LoanRoutes');

const app = express();
app.use(bodyParser.json());

// Connect to MongoDB
mongoose.connect('mongodb://localhost:27017/LMSLoanDB', {
    useNewUrlParser: true,
    useUnifiedTopology: true
});

// Mount loan routes
app.use('/api/loans', loanRoutes);

// Start Express server
app.listen(3000, () => console.log('Server running on port 3000'));
