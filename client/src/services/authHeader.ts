export default function authHeader() {
    let user;
    if(localStorage.getItem('user') == null){
        user = null;
    }
    else{
        user = JSON.parse(localStorage.getItem('user')!);
    }
  
    if (user && user.accessToken) {
      return 'Bearer ' + user.token;
    } else {
      return '';
    }
  }