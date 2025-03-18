// Defines the Loan model in MongoDB.
const mongoose = require('mongoose');

const LoanSchema = new mongoose.Schema({
    user_id: { type: mongoose.Schema.Types.ObjectId, ref: 'User', required: true }, // User reference
    amount: { type: Number, required: true }, // Loan amount
    status: { type: String, enum: ['Pending', 'Approved', 'Rejected', 'Disbursed'], default: 'Pending' },
    created_at: { type: Date, default: Date.now } // Loan creation timestamp
});

module.exports = mongoose.model('Loan', LoanSchema);
