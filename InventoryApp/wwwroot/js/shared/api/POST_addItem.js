export async function addItem(endPoint, content) {

    const request = await fetch(endPoint, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(content)
    })

    if (!request.ok) {
        const error = await request.json();
        throw new Error(error.message || '[addItem] Failed to create Item');
    }
    const data = await request.json();
    console.log(data);
    return data;

}