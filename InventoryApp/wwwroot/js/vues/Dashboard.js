import { deleteItem } from "../shared/api/DELETE_ittem.js";


export async function handleDashboard() {

    const deleteBtns = document.querySelectorAll('.btn_deleteee');

    deleteBtns.forEach(btn => {
        const ptId = btn.dataset.id;
        const ptName = btn.dataset.name;

        btn.addEventListener('click', async () => {

            //confirm('Êtes-vous sûr de vouloir supprimer ce type de produit ?');
            try {
                const data = await deleteItem(ptName, ptId);
                console.log("DELETE SUCCESFULL : ", data)

                btn.closest('.card').remove(); // Exemple pour supprimer une carte


            } catch (error) {
                console.log("Error while DELETE", error.message)
            }
        })
    })
}