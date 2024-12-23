export async function getItems(endpoint) {
    const request = await fetch(endpoint);

    if (!request.ok) {
        const error = await request.json().message;
        throw new Error(error)
    }
    const data = await request.json();
    return data;
}