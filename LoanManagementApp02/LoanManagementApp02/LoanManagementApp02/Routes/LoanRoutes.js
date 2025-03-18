// Defines loan-related API endpoints.
const express = require('express');
const router = express.Router();
const loanController = require('../Controllers/LoanController');

router.post('/apply', loanController.applyLoan);
router.put('/:id/approve', loanController.approveLoan);
router.put('/:id/reject', loanController.rejectLoan);

module.exports = router;
