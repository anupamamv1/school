let idx = 0;
document.getElementById("addRow")?.addEventListener("click", function(){
  const tbody = document.querySelector("#quals tbody");
  const tr = document.createElement("tr");
  tr.innerHTML = `
    <td><input name="Qualifications[${idx}].Course" required></td>
    <td><input name="Qualifications[${idx}].University" required></td>
    <td><input name="Qualifications[${idx}].Year" type="number" required></td>
    <td><input name="Qualifications[${idx}].Percentage" type="number" step="0.01" required></td>
    <td><button type="button" class="del">X</button></td>`;
  tbody.appendChild(tr);
  idx++;
});
document.getElementById("quals")?.addEventListener("click", function(e){
  if (e.target.classList.contains("del")) {
    e.target.closest("tr").remove();
  }
});
