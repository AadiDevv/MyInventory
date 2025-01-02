export async function deleteItem(itemName, id) {

    const url = `/API/${itemName}/${id}`;
    console.log("Delete url : " + url);
    try {
        const response = await fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`Erreur lors de la suppression : ${response.statusText}`);
        }

        // Vérifiez si le statut est 204 (No Content)
        if (response.status === 204) {
            console.log('Suppression réussie: Aucun contenu retourné.');
            return { success: true }; // Retourne un objet vide pour signifier le succès
        }
        // Sinon, essayez de parser le JSON
        const data = await response.json();
        console.log('Suppression réussie:', data);
        return data;

    } catch (error) {
        console.error('Erreur :', error.message);
        throw error;
    }
}