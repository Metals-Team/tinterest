export class PrincipalService {
    async getFacebookToken(){
        return await Expo.SecureStore.getItemAsync('FBToken');
    }
    async setFacebookToken(token){
        return await Expo.SecureStore.setItemAsync('FBToken', token);
    }
    async getAccessToken(){
        console.log('getting acess token');
        const info = {
                method: 'post',
                headers: new Headers({
                    'Content-Type': 'application/json'
                }),
                body: JSON.stringify({
                    access_token: await this.getFacebookToken()
                })
            }
        const response = await fetch('https://tinterest.azurewebsites.net/.auth/login/facebook', info);
        console.log('aceess token response');
        if(response.status != 200) return undefined;
        return (await response.json()).authenticationToken;
    }

    async isLoggedIn(){
        const fbToken = await this.getFacebookToken();
        if(!fbToken){
            return false;
        }
        const x = await this.getAccessToken();
        return !!x;
    }
}