import { getItems } from "../shared/api/GET_getItems.js";
import { addItem } from "../shared/api/POST_addItem.js";
import { updateDropdown, handleDropdown } from "../shared/utils/dropdown.js";
import { showAlert } from "../shared/utils/alert.js";


export async function handleAddProduct() {
    const buttons = document.querySelectorAll('[id^=NewModel]');

    buttons.forEach(button => {

        const modelName = button.id.replace('NewModel', '');
        const input = document.querySelector(`.new${modelName}Input`);
        const selectId = `${modelName}-select`

        handleDropdown(selectId, `new${modelName}_container`);


        button.addEventListener('click', async () => {

            (console.log(`clicked on ${button.id}`))
            const newItemName = input.value;

            
            if (!newItemName) {
                showAlert(`Please enter a ${modelName.toLowerCase()} name.`, 'danger');
                return;
            }

            try {
                await addItem(`/API/${modelName}`, { name : newItemName });
                await updateDropdown(selectId, modelName, newItemName);


            } catch (error) {

                console.log(error.message);
                showAlert(error.message, 'danger');
            }
            input.value = "";
            const formContainer = document.getElementById(`new${modelName}_container`);
            formContainer.style.display = 'none'

        })
    })
}