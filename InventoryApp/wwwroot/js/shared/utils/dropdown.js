import { getItems } from "../api/GET_getItems.js";

export async function updateDropdown(selectId, modelName, newValue ) {

    const items = await getItems(`/API/${modelName}`);

    const selectTag = document.getElementById(selectId);
    selectTag.innerHTML = `<option>-- Select option --</option>`;

    console.log('[updateDropdown] Items = ' + items);

    items.forEach(item => {
        const option = document.createElement('option');
        option.value = item.id;;
        option.textContent = item.name;

        if (item.name == newValue) {
            console.log('[updateDropdown]' + `new vale (${newValue}) ; name (${item.name})`);

            option.selected = true;
        }
        selectTag.appendChild(option);
    })
    const addItemTag = document.createElement('option');
    addItemTag.value = 'add-new';
    addItemTag.textContent = '+ add-new';
    selectTag.appendChild(addItemTag);
   


}

export function handleDropdown(selectId, formContainerId, addOptionValue = 'add-new') {
    const dropDown = document.getElementById(selectId);

    if (!dropDown) {
        console.error(`Élément avec l'ID "${selectId}" introuvable.`);
        return;
    }


    dropDown.addEventListener('change', (event) => {
        console.log("changed")
        const formContainer = document.getElementById(formContainerId);
        if (!formContainerId) {
            console.error(`Élément avec l'ID "${formContainer}" introuvable.`);
            return;
        }
        if (event.target.value === addOptionValue) {
            formContainer.style.display = 'block'
        } else {
            formContainer.style.display = 'none'

        }

    })
}
