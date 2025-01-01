//import { getCurrentUser } from "../shared/api/GET_account.js";

//export async function handleConnectionNavOutput() {
//    const ulTag = document.getElementById("connection-ul");
//    if (!ulTag) {
//        console.error("Element with ID 'connection-ul' not found.");
//        return;
//    }

//    let user = JSON.parse(localStorage.getItem('currentUser'));

//    if (!user) {
//        user = await getCurrentUser();
//        if (user) {
//            localStorage.setItem('currentUser', JSON.stringify(user));
//        }
//    }
//    console.log("out " + user.username);

//    if (user) {
//        console.log("im in " + user.username);

//        ulTag.innerHTML = ` <p>${user.username}</p>
//            <form action="/Account/Logout" method="post" style="display:inline;" onsubmit="localStorage.removeItem('currentUser')">
//                <button type="submit" class="btn text-danger">Logout</button>
//            </form>
//        `;
//    } else {
//        ulTag.innerHTML = `
//            <li class="nav-item">
//                <a class="nav-link btn btn-outline-primary me-2" href="/Account/Login">Login</a>
//            </li>
//            <li class="nav-item">
//                <a class="nav-link btn btn-primary text-light" href="/Account/Register">Register</a>
//            </li>
//        `;
//        localStorage.removeItem('currentUser'); // Nettoie si pas d'utilisateur
//    }
//}
