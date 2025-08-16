package main

import (
	"fmt"
	//"encoding/json"
    //"io/ioutil"
    "math"
    "strconv"
)

func main(){

	//fmt.Println("START")
    //byteValue, err := ioutil.ReadFile("API_ARGUMENT.json")
    //if err != nil {
    //    panic(err)
    //}

    //var f interface{}

    //err = json.Unmarshal(byteValue, &f)

    //if err != nil {
    //    panic(err)
    //}

    //m := f.(map[string]interface{})
    //fmt.Println(m)
    //create_vector(10)
	
	var mas []map[string]int
	
	mas2 := map[string]int{"asd":1,"dsa":2}
	for i := 0; i< 5;i++{
		newMap := make(map[string]int)
		for key,value := range(mas2){
		newMap[key] = value
		}
		mas = append(mas,newMap)
	}
	
	mas[0]["asd"] = 5
	fmt.Println(mas)
	}

func create_vector(len_vector int)([]string){
    var mas []string
    for i:=0; i < int(math.Pow(2,float64(len_vector))); i++{
        bin_value, _ := ConvertInt(fmt.Sprint(i),10,2)
        fmt.Println(bin_value)
    }
    return mas
}

func ConvertInt(val string, base, toBase int) (string, error) {
    i, err := strconv.ParseInt(val, base, 64)
    if err != nil {
        return "", err
    }
    return strconv.FormatInt(i, toBase), nil
}