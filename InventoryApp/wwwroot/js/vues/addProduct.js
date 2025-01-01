import { getItems } from "../shared/api/GET_getItems.js";
import { addItem } from "../shared/api/POST_addItem.js";
import { updateDropdown, handleDropdown } from "../shared/utils/dropdown.js";
import { showAlert } from "../shared/utils/alert.js";
import { getCurrentUser } from "../shared/api/GET_account.js";



export async function handleAddProduct() {
    const buttons = document.querySelectorAll('[id^=NewModel]');

    const user = await getCurrentUser();

    buttons.forEach(button => {

        const modelName = button.id.replace('NewModel', '');
        const input = document.querySelector(`.new${modelName}Input`);
        const selectId = `${modelName}-select`;

        if (modelName === 'ProductType') {
            handleDropdown(selectId, 'newSection', 'add-new', true, user.id);            // Gérer l'affichage de la section principale (newSection)
            handleDropdown(selectId, `new${modelName}_container`);            // Gérer l'affichage du formulaire d'ajout spécifique (newProductType_container)
        } else {
            handleDropdown(selectId, `new${modelName}_container`, 'add-new');            // Gérer les autres cas (Category, Supplier)

        }


        button.addEventListener('click', async () => {
            const newItemName = input.value;
            const addItemContainer = document.getElementById(`new${modelName}_container`);
            const productTypeId = addItemContainer.dataset.productTypeId;

            if (!newItemName) {
                showAlert(`Please enter a ${modelName.toLowerCase()} name.`, 'danger');
                return;
            }

            try {

                const isProductType = (modelName === 'ProductType');                // Déterminer si on ajoute l'option "data-trigger = 'true'" dans les optionTag (pour ProductType uniquement)

                if (isProductType) {
                    await addItem(`/API/${modelName}`, { name: newItemName, UserId: user.id });
                    await updateDropdown(selectId, modelName, newItemName, { UserId: user.id}, isProductType);       // Mettre à jour le dropdown avec le nouvel élément

                } else {
                    if (!productTypeId) {
                        console.error('❌ ProductTypeId is undefined or null.');
                        return;
                    }

                    await addItem(`/API/${modelName}`, { name: newItemName, ProductTypeId: productTypeId }); // ON AJOUTER EGALEMENT USER.ID MON ON ARETTE pour eviter la redondance. On peut y acceder via son productType
                    await updateDropdown(selectId, modelName, newItemName, { ProductTypeId: productTypeId}, isProductType);       // Mettre à jour le dropdown avec le nouvel élément, en filtrant

                }




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