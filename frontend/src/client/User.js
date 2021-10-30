import axios from "axios";

export default {
    Profile(){
       return axios.get(`Users/Profile`);
    },
    UpdateProfile(data){
        return axios.put(`Users`,data);
     }  
}