export async function getCurrentUser() {

    try {
        const request = await fetch("/Account/Profile");

        if (!request.ok) {
            console.log("Error while fetching");
            return;

        }

        const data = await request.json();
        console.log(data)

        return data
    } catch (e) {
        console.log(e)
    }

}