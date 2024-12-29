import { getItems } from "../api/GET_getItems.js";

export async function updateDropdown(selectId, modelName, newValue, filltreId = null, isTriggerd = false) {

    // Par default l'url
    let url = `/API/${modelName}`;

    console.log("fillteredId = " + filltreId);

    // Si l'on souhaite filtrer par id
    if (filltreId != null) {
        url += `/ByProductType?ProductTypeId=${filltreId}`
    }
    const items = await getItems(url);

    const selectTag = document.getElementById(selectId);
    if (selectTag) {
        selectTag.innerHTML = `<option>-- Select option --</option>`;
    }
    else {
        console.log('El Select tag not found')
    }

    console.log('[updateDropdown] Items = ', items);

    if (!Array.isArray(items)) {
        console.warn('⚠️ Les items ne sont pas un tableau valide. Vérifiez la réponse de l\'API.');
        return;
    }

    // Vérifier si le tableau est vide
    if (items.length === 0) {
        console.warn('⚠️ Aucun élément trouvé pour le modèle demandé.');
    }

    items.forEach(item => {
        const option = document.createElement('option');
        option.value = item.id;;
        option.textContent = item.name;
        if (isTriggerd) {
            option.dataset.trigger = 'true';
        }

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

async function handleNewSection(productTypeId, subContainers) {
    console.log('handleNewSection : in the handleNewSection ')

    // Mettre à jour les sélecteurs filtrés par ProductTypeId
    try {
        subContainers.forEach(async (el) => {
            await updateDropdown(`${el}-select`, el, null, productTypeId);
            document.getElementById(`new${el}_container`).dataset.productTypeId = productTypeId; // Stocker le ProductTypeId dans les containers
        })
    } catch (error) {
        console.error('❌ Erreur lors du fetch des données filtrées :', error.message);
    }
}

// Handle The input display -> Block or None
export function handleDropdown(selectId, formContainerId, addOptionValue = 'add-new', isNewSection = false) {
    const dropDown = document.getElementById(selectId);
    const subContainers = ['Category', 'Supplier'];

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

        const selectedOption = event.target.selectedOptions[0];
        const selectedoptionId = event.target.value;

        if (isNewSection) {
            if (selectedOption && selectedOption.dataset.trigger === 'true') {
                handleNewSection(selectedoptionId, subContainers);
                formContainer.style.display = 'block';

            } else {
                formContainer.style.display = 'none';
                // Réinitialiser les ID dans les conteneurs
                subContainers.forEach(container => {
                    document.getElementById(`new${container}_container`).dataset.productTypeId = '';
                })
            }
        } else {
            if (event.target.value === addOptionValue) {
                formContainer.style.display = 'block'
            } else {
                formContainer.style.display = 'none'

            }
        }
    

    })
}
