import { updateDropdown } from "./shared/utils/dropdown.js";
import { handleAddProduct } from "./vues/addProduct.js";
import { submitForm } from "./shared/api/POST_form.js";

document.addEventListener('DOMContentLoaded', async () => { 
    console.log("hello");

    const productTypeEl = document.getElementById('ProductType-select');
    if (productTypeEl) {
        await updateDropdown('ProductType-select', 'ProductType', null, null, true);
    }
    else {
        console.log('El Select tag not found')
    }

    submitForm('addProduct', '/API/Product',
        (result) => {alert('Product added sccesfully');
        console.log(result.message);},
        (result) => {alert('Failed to add Product');
                console.log(result.message);}
   )

    handleAddProduct();
});
