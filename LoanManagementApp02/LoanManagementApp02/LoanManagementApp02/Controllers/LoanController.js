// Loan management logic for applying, approving, and rejecting loans
const Loan = require('../Models/Loan');

// Apply for a Loan
exports.applyLoan = async (req, res) => {
    const { user_id, amount } = req.body;

    const loan = new Loan({
        user_id,
        amount,
        status: 'Pending'
    });

    await loan.save();
    res.status(201).json({ message: 'Loan application submitted.' });
};

// Approve Loan
exports.approveLoan = async (req, res) => {
    const { id } = req.params;
    await Loan.findByIdAndUpdate(id, { status: 'Approved' });
    res.status(200).json({ message: 'Loan approved.' });
};

// Reject Loan
exports.rejectLoan = async (req, res) => {
    const { id } = req.params;
    await Loan.findByIdAndUpdate(id, { status: 'Rejected' });
    res.status(200).json({ message: 'Loan rejected.' });
};
