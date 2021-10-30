import axios from "axios";
export default {
  Search(word, ofset, limit) {
    return axios.post(`Search/` + word + `,` + ofset + `,` + limit);
  },
};
