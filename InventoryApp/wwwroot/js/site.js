import { updateDropdown } from "./shared/utils/dropdown.js";
import { handleAddProduct } from "./vues/addProduct.js";
import { submitForm } from "./shared/api/POST_form.js";

document.addEventListener('DOMContentLoaded', async () => { 
    console.log("hello");


    // AddProduct.cshtml view

    const controllers = ['Supplier', 'Category'];
    for (const controller of controllers) {
        if (document.getElementById(`${controller}-select`)){
            await updateDropdown(`${controller}-select`, controller);
        }
    }

    submitForm('addProduct', '/API/Product',
        (result) => {alert('Product added sccesfully');
        console.log(result.message);},
        (result) => {alert('Failed to add Product');
                console.log(result.message);}
   )

    handleAddProduct();
});
