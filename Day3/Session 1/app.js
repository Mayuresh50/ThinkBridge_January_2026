function calculateTotal(price, tax) {
	let total = price + tax;
	return total;
}

let price = 100;
let tax = 18;
let finalAmount = calculateTotal(price, tax);

console.log("Final Amount:", finalAmount);
