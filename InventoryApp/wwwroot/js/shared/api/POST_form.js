export async function submitForm(formId, url, onSuccess, onError) {
    const form = document.getElementById(formId);

    form.addEventListener('submit', async (e) => {
        e.preventDefault();
        const newFormData = new FormData(form); // get the form data
        console.log(newFormData)
        const formJson = Object.fromEntries(newFormData.entries());

        try {
            const requete = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formJson)
            });
            if (!requete.ok) {
                const result = await requete.json(); 
                console.log(`❌ result:`, result);

                console.error(`❌ Error Message:`, result.message || 'Unknown error occurred');
                if (result.error) {
                    console.error('⚠️ Technical Error:', result.error);
                }
                if (result.stackTrace) {
                    console.error('🛠️ Stack Trace:', result.stackTrace);
                }
                if (result.errors) {
                    console.error('🔍 Validation Errors:', result.errors);
                }

                if (onError) onError(result);
                return; 
            }
            const result = await requete.json(); 
            console.log('✅ Message:', result.message || 'Operation succeeded');
            console.log('📝 Data:', result.data || 'No data provided');
            window.location.reload();
            if (onSuccess) onSuccess(result);
        } catch (err) {
            console.error(`🚨 Network error: caught while submitting ${formId} on the endpoint ${url}`, err.message);
            if (onError) onError(err);
        }
    });
}
