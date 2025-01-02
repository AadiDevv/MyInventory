import { updateDropdown } from "./shared/utils/dropdown.js";
import { getCurrentUser } from "./shared/api/GET_account.js";

import { handleAddProduct } from "./vues/addProduct.js";
import { submitForm } from "./shared/api/POST_form.js";
import { handleDashboard } from "./vues/Dashboard.js"

document.addEventListener('DOMContentLoaded', async () => {
    console.log("hello");



    //let user = JSON.parse(localStorage.getItem('currentUser'));

    //if (!user) {
       let user = await getCurrentUser();
    //    if (user) {
    //        localStorage.setItem('currentUser', JSON.stringify(user));
    //    }
    //}

    const productTypeEl = document.getElementById('ProductType-select');
    if (productTypeEl) {
        console.log("User id = " + user.id)
        console.log("User  = " , user)

        await updateDropdown('ProductType-select', 'ProductType', null, { UserId: user.id }, true);
    }
    else {
        console.log('El Select tag not found')
    }

    submitForm('addProduct', '/API/Product',
        (result) => {
            alert('Product added sccesfully');
            console.log(result.message);
        },
        (result) => {
            alert('Failed to add Product');
            console.log(result.message);
        }
    )

    handleAddProduct();
    await handleDashboard()
});
